using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.core.Entities
{
    public class AzureCognitiveServicesSettings
    {
        public string ServicesApiKey { get; set; }
        public string TagsApiEndpoint { get; set; }
        public string ModerationEvaluteApiEndpoint { get; set; }
        public string OkTestImageUrl { get; set; }
        public string NotOkTestImageUrl { get; set; }
        public bool ForceNotOkResponse { get; set; }

    }
}