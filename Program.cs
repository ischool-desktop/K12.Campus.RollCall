using FISCA;
using FISCA.Presentation;
using FISCA.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Campus.RollCall
{
    public class Program
    {
        [MainMethod()]
        static public void Main()
        {
            //tool._A.Select<AbsenceUDT>();
            //tool._A.Select<RollCallLog>();

            //ServerModule.AutoManaged("http://module.ischool.com.tw/module/138/OnlineNamed/udm.xml");

            //Print["點名節次缺曠別設定"].Click += delegate
            //{
            //    ConfigFrom cf = new ConfigFrom();
            //    cf.ShowDialog();

            //};
            {
                RoleAclSource.Instance["學務作業"]["功能按鈕"].Add(new RibbonFeature("9BFBE652-0E3C-4447-8DC5-AAB654872565", "線上點名設定"));
                FISCA.Presentation.MotherForm.RibbonBarItems["學務作業", "基本設定"]["設定"]["線上點名設定"].Enable = FISCA.Permission.UserAcl.Current["9BFBE652-0E3C-4447-8DC5-AAB654872565"].Executable;
                FISCA.Presentation.MotherForm.RibbonBarItems["學務作業", "基本設定"]["設定"]["線上點名設定"].Click += delegate
                {
                    OnlineRollCall rollCall = new OnlineRollCall();
                    rollCall.ShowDialog();
                };
            }


        }
    }
}
