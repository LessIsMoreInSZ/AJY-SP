using EF.Models.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPWindowsForms.DbService
{
    public class DownloadSampleModel
    {
        
        public string startpoint { get; set; }
  
        public string endpoint { get; set; }
      
        public string chuisaoyali { get; set; }
      
        public string duoduanjiance1 { get; set; }
       
        public string duoduanjiance2 { get; set; }
        
        public string duoduanjiance3 { get; set; }
      
        public string duoduanjiance4 { get; set; }
    
        public string duoduanjiance5 { get; set; }
      
        public string fanying { get; set; }
    
        public string tongfeng { get; set; }
      
        public string fengbi { get; set; }
       
        public string start_yeya_PG { get; set; }
        
        public string stop_yeya_PG { get; set; }
       
        public string start_PG_tank { get; set; }
      
        public string end_PG_tank { get; set; }
        public float? CA_PG { get; set; }
        public float? zongchouqi { get; set; }
        
        public string usechanle { get; set; }
 
        public string pfname { get; set; }
    
        public string DCM_id { get; set; }
  
        public string product_id { get; set; }
     
        public string select_paiqi { get; set; }
      
        public string select_yeya { get; set; }
       
        public string select_jixie { get; set; }
      
        public string duoduanjiance1_hmienable { get; set; }
       
        public string duoduanjiance2_hmienable { get; set; }
     
        public string duoduanjiance3_hmienable { get; set; }
   
        public string duoduanjiance4_hmienable { get; set; }
     
        public string duoduanjiance5_hmienable { get; set; }
        
        public DateTime update_time { get; set; } 
    }
    public class SearchSampleModel
    {
        public int id { get; set; }
        public string product_id { get; set; }
    }
    public class SearchSamples
    {
        public SampleTable sample { get; set; }
        public List<SampleTable> last_samples { get; set; } = new List<SampleTable>();
    }
}
