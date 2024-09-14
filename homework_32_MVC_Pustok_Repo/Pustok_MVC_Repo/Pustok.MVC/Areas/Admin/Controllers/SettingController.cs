using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using MongoDB.Bson;
using MongoDB.Driver;
using NuGet.Configuration;
using Pustok.Business.ViewModels;
using Pustok.Core.Models;
using Pustok.Data.DAL;
using StackExchange.Redis;

namespace Pustok.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDatabase _database;
        private readonly MongoDbContext _mongoDbContext;

        public SettingController(AppDbContext appDbContext, IConnectionMultiplexer redis)
        {
            _appDbContext = appDbContext;
            _database = redis.GetDatabase();
            _mongoDbContext = new MongoDbContext();
        }
        public async Task<IActionResult> Index()
        {
            return View(await _appDbContext.Settings.ToListAsync());
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            await _appDbContext.Settings.AddAsync(setting);
            await _appDbContext.SaveChangesAsync();

            var hashEntries = new HashEntry[] { new HashEntry(setting.Key, setting.Value) };
            await _database.HashSetAsync("Settings", hashEntries);

            SettingMongoDb settingMongoDb = new()
            {
                Key = setting.Key,
                Value = setting.Value,
            };

            await _mongoDbContext.Settings.InsertOneAsync(settingMongoDb);

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(string key)
        {
            var data = await _appDbContext.Settings.FirstOrDefaultAsync(x => x.Key == key) ?? throw new NullReferenceException();
            return View(data);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(string key, Setting setting)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var data = await _appDbContext.Settings.FirstOrDefaultAsync(x => x.Key == key) ?? throw new NullReferenceException();

            data.Value = setting.Value;

            await _appDbContext.SaveChangesAsync();

            await _database.HashSetAsync("Settings", new HashEntry[] { new HashEntry(setting.Key, setting.Value) });

            var filter = Builders<SettingMongoDb>.Filter.Eq(x=>x.Key, key);
            var existingDocument = await _mongoDbContext.Settings.Find(filter).FirstOrDefaultAsync();

            SettingMongoDb settingMongoDb = new()
            {
                Id = existingDocument.Id,
                Key = setting.Key,
                Value = setting.Value,
            };
            var updateResult = await _mongoDbContext.Settings.FindOneAndReplaceAsync(filter, settingMongoDb);


            return RedirectToAction(nameof(Index));
        }


    }
}
