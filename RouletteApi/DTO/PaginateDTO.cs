using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.DTO
{
    public class PaginateDTO
    {
        public int Page { get; set; } = 1;
        private int rowsByPage = 10;
        private readonly int maxRowsByPage = 50;
        public int RowsByPage {
            get => rowsByPage;
            set
            {
                rowsByPage = (value > maxRowsByPage) ? maxRowsByPage : value;
            }
        }
    }
}
