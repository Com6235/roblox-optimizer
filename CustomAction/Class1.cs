using System;
using Microsoft.Deployment.WindowsInstaller;
using System.Windows.Forms;

namespace DLL
{
    public class DLLClass
    {
        public DLLClass(Session session)
        {
            DLLMain(session);
        }

        [CustomAction]
        public static ActionResult DLLMain(Session session)
        {
            if (session.CustomActionData != null)
            {
                MessageBox.Show(session.CustomActionData.ToString());
                return ActionResult.Success;
            } else
            {
                return ActionResult.Failure;
            }
        }
    }
}
