using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Campus.RollCall
{
    [TableName("campus.rollcall.allow_absence")]
    class AbsenceUDT : ActiveRecord
    {
        /// <summary>
        /// 缺曠別
        /// </summary>
        [Field(Field = "absence", Indexed = true)]
        public string Absence { get; set; }

        /// <summary>
        /// 節次
        /// </summary>
        [Field(Field = "period", Indexed = true)]
        public string Period { get; set; }
    }
}
