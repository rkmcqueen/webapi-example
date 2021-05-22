using keyapi.Controllers;
using keyapi.Models;
using keyapi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiTest
{
    public class Tests
    {
        public KeyController _keyController { get; set; }
        public ApiKey _createdKey { get; set; }
        [SetUp]
        public void Setup()
        {
            List<ApiKey> apiKeys = new List<ApiKey>
                {
                    new ApiKey("Vendor A"){ Id = 1},
                    new ApiKey("Vendor B"){ Id = 2}
                };

            Mock<IKeyRepository> mockRepository = new Mock<IKeyRepository>();

            // Return all the keys
            mockRepository.Setup(mr => mr.GetAsync(null)).Returns(Task.FromResult(apiKeys));
            // Return vendor A keys
            mockRepository.Setup(mr => mr.GetAsync("Vendor A")).Returns(Task.FromResult(apiKeys.Where(key => key.Vendor == "Vendor A").ToList()));
            // Invalid vendor keys
            mockRepository.Setup(mr => mr.GetAsync("Vendor C")).Returns(Task.FromResult(new List<ApiKey>()));
            _keyController = new KeyController(mockRepository.Object);
            // Return specific key
            mockRepository.Setup(mr => mr.GetByIdAsync(1)).Returns(Task.FromResult(apiKeys.FirstOrDefault(key => key.Id == 1)));
            // Invalid key
            mockRepository.Setup(mr => mr.GetByIdAsync(3)).Returns(Task.FromResult<ApiKey>(null));
            // Create Key
            _createdKey = new ApiKey("Created Key") { Id = 3 };
            mockRepository.Setup(mr => mr.StoreApiKeyAsync(_createdKey.Vendor, null)).Returns(Task.FromResult<ApiKey>(_createdKey));
            _keyController = new KeyController(mockRepository.Object);
        }

        [Test]
        public async Task GetAll()
        {
            List<ApiKey> apiKeys = (List<ApiKey>)await _keyController.Get();
            Assert.AreEqual(apiKeys.Count(), 2);
        }

        [Test]
        public async Task GetById()
        {
            ApiKey apiKey = (ApiKey)await _keyController.GetById(1);
            Assert.AreEqual(apiKey.Id, 1);
        }


        [Test]
        public async Task GetByVendor()
        {
            List<ApiKey> apiKeys = (List<ApiKey>)await _keyController.Get("Vendor A");
            Assert.AreEqual(apiKeys.Count(), 1);
        }

        [Test]
        public async Task GetUnknownVendor()
        {
            var error = await _keyController.Get("Vendor C");
            Assert.AreEqual(error.GetType(), typeof(NotFoundObjectResult));
        }

        [Test]
        public async Task GetUnknownId()
        {
            var error = await _keyController.GetById(3);
            Assert.AreEqual(error.GetType(), typeof(NotFoundObjectResult));
        }

        [Test]
        public async Task CreateKeyRequiresVendor()
        {
            var error = await _keyController.CreateApiKey(string.Empty, null);
            Assert.AreEqual(error.GetType(), typeof(BadRequestObjectResult));
        }
        [Test]
        public async Task CreateKey()
        {
            ApiKey apiKey = (ApiKey)await _keyController.CreateApiKey(_createdKey.Vendor);
            Assert.AreEqual(apiKey.Vendor, _createdKey.Vendor);
            Assert.AreEqual(apiKey.Key, _createdKey.Key);
        }
    }
}