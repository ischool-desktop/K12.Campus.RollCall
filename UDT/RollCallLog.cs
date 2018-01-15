using FISCA.UDT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace K12.Campus.RollCall
{
    [TableName("campus.rollcall.log")]
    class RollCallLog : ActiveRecord
    {
        /// <summary>
        /// 老師系統編號
        /// </summary>
        [Field(Field = "ref_teacher_id", Indexed = true)]
        public int TeacherID { get; set; }

        /// <summary>
        /// 內容
        /// </summary>
        [Field(Field = "content", Indexed = true)]
        public string Content { get; set; }
    }
}
