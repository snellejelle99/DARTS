using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using DARTS.ViewModel.Command;

namespace DARTS_UnitTests.ViewModel
{
    [TestClass]
    public class RelayCommand_UnitTests
    {
        [TestMethod]
        public void RelayCommand_Should_Not_Throw_With_Empty_CanExecute()
        {
            RelayCommand cmd = new RelayCommand(execute => { });

            try
            {
                Assert.IsTrue(cmd.CanExecute(new object()), "CanExecute did not return true when no CanExexute func is supplied");
            }
            catch(NullReferenceException e)
            {
                Assert.Fail($"Nullreference exception occurred: {e.Message} \n {e.StackTrace}");                
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
