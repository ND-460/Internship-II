using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Entities.Models.CommonModels
{
    public class DropDownResponseModel
    {
        public int Value { get; set; }

        public string Text { get; set; }

        public DropDownResponseModel(int value, string text)
        {
            Value = value;
            Text = text;
        }
    }
}
