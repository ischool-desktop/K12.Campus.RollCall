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

            #region 線上點名設定
            {
                RoleAclSource.Instance["學務作業"]["功能按鈕"].Add(new RibbonFeature("9BFBE652-0E3C-4447-8DC5-AAB654872565", "線上點名設定"));
                FISCA.Presentation.MotherForm.RibbonBarItems["學務作業", "基本設定"]["設定"]["線上點名設定"].Enable = FISCA.Permission.UserAcl.Current["9BFBE652-0E3C-4447-8DC5-AAB654872565"].Executable;
                FISCA.Presentation.MotherForm.RibbonBarItems["學務作業", "基本設定"]["設定"]["線上點名設定"].Click += delegate
                {
                    OnlineRollCall rollCall = new OnlineRollCall();
                    rollCall.ShowDialog();
                };
            }
            #endregion
            
            #region 課堂點名檢視/管理
            {
                RoleAclSource.Instance["學務作業"]["功能按鈕"].Add(new RibbonFeature("4BF97B3D-827A-4373-B8B0-A3A3719E6A7A", "課堂點名檢視/管理"));
                MotherForm.RibbonBarItems["學務作業", "批次作業/查詢"]["課堂點名檢視/管理"].Enable = UserAcl.Current["4BF97B3D-827A-4373-B8B0-A3A3719E6A7A"].Executable;
                MotherForm.RibbonBarItems["學務作業", "批次作業/查詢"]["課堂點名檢視/管理"].Image = Properties.Resources.admissions_ok_64;
                MotherForm.RibbonBarItems["學務作業", "批次作業/查詢"]["課堂點名檢視/管理"].Size = RibbonBarButton.MenuButtonSize.Medium;
                MotherForm.RibbonBarItems["學務作業", "批次作業/查詢"]["課堂點名檢視/管理"].Click += delegate
                {
                    (new RollCallManager()).ShowDialog();
                };
            }
            #endregion

        }
    }
}
