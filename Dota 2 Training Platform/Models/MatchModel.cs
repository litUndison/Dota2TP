using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class MatchModel
{
    public string match_id { get; set; }
    public bool radiant_win { get; set; }
    public string duration { get; set; }
    public string game_mode { get; set; }
    public string start_time { get; set; }
    public string kills { get; set; }
    public string deaths { get; set; }
    public string assists { get; set; }
}
