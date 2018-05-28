using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class OtherRiverwayInfo
    {
        //工程基本信息
        public string s_id { get; set; }
        public string s_name { get; set; }
        public string s_project_no { get; set; }
        public string n_year { get; set; }
        public string n_pace_status { get; set; }
        public string s_town { get; set; }
        public string s_address { get; set; }
        public string s_legal_person { get; set; }
        public string s_unit_design { get; set; }
        public string s_unit_build { get; set; }
        public string s_unit_supervise { get; set; }
        public string n_reckon_total_amt { get; set; }
        public string n_water_area { get; set; }
        public string s_remark { get; set; }
        public string n_draft { get; set; }
        public string remove_pic_ids { get; set; }

        //批复工程量
        public string s_content { get; set; }
        public string s_gkpf { get; set; }
        public string s_cspf { get; set; }
        public string s_zjpw { get; set; }

        //概算投资
        public string n_total_invest { get; set; }
        public string n_engin_cost { get; set; }
        public string n_independent_cost { get; set; }
        public string n_prep_cost { get; set; }
        public string n_sight_cost { get; set; }
        public string n_build_cost { get; set; }

        //资金配套组成
        public string n_subsidy_city { get; set; }
        public string n_subsidy_district { get; set; }
        public string n_subsidy_town { get; set; }

        //完成工程量
        public string s_complete_content { get; set; }
    }
}