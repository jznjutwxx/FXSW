using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class BackboneRiverwayInfo
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

        //批复工程量
        public string n_length { get; set; }
        public string n_land_area { get; set; }
        public string n_protect_hard { get; set; }
        public string n_protect_ecology { get; set; }
        public string n_bridge { get; set; }
        public string n_plant_bank { get; set; }
        public string n_plant_slope { get; set; }
        public string n_plant_depth { get; set; }
        public string n_river_count { get; set; }

        //概算投资
        public string n_total_invest { get; set; }
        public string n_engin_cost { get; set; }
        public string n_independent_cost { get; set; }
        public string n_prep_cost { get; set; }
        public string n_sight_cost { get; set; }
        public string n_empty_area { get; set; }
        public string n_build_cost { get; set; }

        //资金配套组成
        public string n_subsidy_city { get; set; }
        public string n_subsidy_district { get; set; }
        public string n_subsidy_town { get; set; }

        //完成工程量
        public string n_complete_length { get; set; }
        public string n_complete_land_area { get; set; }
        public string n_complete_protect_hard { get; set; }
        public string n_complete_protect_ecology { get; set; }
        public string n_complete_bridge { get; set; }
        public string n_complete_plant_bank { get; set; }
        public string n_complete_plant_slope { get; set; }
        public string n_complete_plant_depth { get; set; }
        public string n_complete_river_count { get; set; }


    }
}