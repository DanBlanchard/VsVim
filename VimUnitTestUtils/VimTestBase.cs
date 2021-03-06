﻿using System.Linq;
using NUnit.Framework;
using System;

namespace Vim.UnitTest
{
    /// <summary>
    /// Standard test base for vim services which wish to do standard error monitoring like
    ///   - No dangling transactions
    ///   - No silent swallowed MEF errors
    /// </summary>
    [TestFixture]
    public abstract class VimTestBase
    {
        private IVimErrorDetector _vimErrorDetector;

        [SetUp]
        public void SetupBase()
        {
            _vimErrorDetector = EditorUtil.FactoryService.VimErrorDetector;
            _vimErrorDetector.Clear();
        }

        [TearDown]
        public void TearDownBase()
        {
            if (_vimErrorDetector.HasErrors())
            {
                var msg = String.Format("Extension Exception: {0}", _vimErrorDetector.GetErrors().First().Message);
                Assert.Fail(msg);
            }
        }
    }
}
