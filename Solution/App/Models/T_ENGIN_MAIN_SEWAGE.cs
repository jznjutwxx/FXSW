using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class T_ENGIN_MAIN_SEWAGE
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
        //批复工程量
        public string N_CZSL { get; set; }
        public string N_ZHS { get; set; }
        public string N_JDCLHS { get; set; }
        public string N_SZNGHS { get; set; }
        public string N_GWCD { get; set; }
        public string N_JDCLZ { get; set; }
        public string N_XFJHFC { get; set; }
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
        public string N_WCCZSL { get; set; }
        public string N_WCZHS { get; set; }
        public string N_WCJDCLHS { get; set; }
        public string N_WCSZNGHS { get; set; }
        public string N_WCGWCD { get; set; }
        public string N_WCJDCLZ { get; set; }
        public string N_WCXFJHFC { get; set; }
    }
}