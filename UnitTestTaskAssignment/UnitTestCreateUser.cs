namespace UnitTestTaskAssignment
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using TaskingSystem.Server;

    [TestClass]
    public class UnitTest1
    {
        private readonly IUserRepository _userRepository;

        public UnitTest1(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [TestMethod]
        public void TestCreateUser()
        {
            _userRepository.
        }
    }
}
