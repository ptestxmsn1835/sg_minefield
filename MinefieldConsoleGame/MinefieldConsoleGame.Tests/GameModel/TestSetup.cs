using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace MinefieldConsoleGame.Tests.GameModelTests
{
    public class TestSetup
    {
        public Mock<IIOHelper> IOHelperMock { get; private set; }

        [SetUp]
        public void Setup()
        {
            IOHelperMock = new Mock<IIOHelper>();
            IOHelperMock.Setup(s => s.Write(It.IsAny<string>()));
            IOHelperMock.Setup(s => s.GetCharInput(It.IsAny<string>(), It.IsAny<List<ConsoleKey>>())).Returns(ConsoleKey.Y);
            IOHelperMock.Setup(s => s.GetNumericInput("Live Count:", 0, 10)).Returns(0);
        }
    }
}
