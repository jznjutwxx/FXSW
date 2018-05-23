using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class T_ENGIN_INFO
    {
        //工程基本信息
        public string S_ID { get; set; }
         public string S_NAME { get; set; }
        public string N_TYPE { get; set; }
        public string S_PROJECT_NO { get; set; }
        public string N_YEAR { get; set; }
        public string N_PACE_STATUS { get; set; }
        public string S_TOWN { get; set; }
        public string S_ADDRESS { get; set; }
        public string S_LEGAL_PERSON { get; set; }
        public string S_UNIT_DESIGN { get; set; }
        public string S_UNIT_BUILD { get; set; }
        public string S_UNIT_SUPERVISE { get; set; }
        public string N_RECKON_TOTAL_AMT { get; set; }
        public string S_REMARK { get; set; }
        public string REMOVE_PIC_IDS { get; set; }

        //批复工程量
        public string N_GGSL { get; set; }
        public string N_GGMJ { get; set; }
        public string N_BZZS { get; set; }
        public string N_BZTT { get; set; }
        public string N_DXQD { get; set; }
        public string N_DC { get; set; }
        public string N_DXH { get; set; }
        public string N_CQMQ { get; set; }
        public string N_XFJDL { get; set; }

        //概算投资
        public string N_TOTAL_INVEST { get; set; }
        public string N_ENGIN_COST { get; set; }
        public string N_INDEPENDENT_COST { get; set; }
        public string N_PREP_COST { get; set; }
        public string N_SIGHT_COST { get; set; }

        //资金配套组成
        public string N_SUBSIDY_CITY { get; set; }
        public string N_SUBSIDY_DISTRICT { get; set; }
        public string N_SUBSIDY_TOWN { get; set; }

        //完成工程量
        public string N_WCGGSL { get; set; }
        public string N_WCGGMJ { get; set; }
        public string N_WCBZZS { get; set; }
        public string N_WCBZTT { get; set; }
        public string N_WCDXQD { get; set; }
        public string N_WCDC { get; set; }
        public string N_WCDXH { get; set; }
        public string N_WCCQMQ { get; set; }
        public string N_WCXFJDL { get; set; }
    }
}