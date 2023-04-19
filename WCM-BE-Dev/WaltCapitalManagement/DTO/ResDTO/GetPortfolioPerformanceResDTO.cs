﻿namespace DTO.ResDTO
{
    public class GetPortfolioPerformanceResDTO
    {
        public List<GraphDataModelList> dataModelList { get; set; }
        public List<Dictionary<string, string>> GraphDataModelValueList { get; set; }

        public class GraphDataModelList
        {
            public string Label { get; set; }
            public string Value { get; set; }
        }

    }
}
