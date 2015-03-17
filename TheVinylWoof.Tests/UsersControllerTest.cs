using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheVinylWoof.Controllers;

namespace TheVinylWoof.Tests
{
    [TestClass]
    public class UsersControllerTest
    {
        [TestMethod]
        public void UserControllerGet()
        {
            var ctrl = new UsersController(new FakeVinylWoofRepo());
        }
    }
}
