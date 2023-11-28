namespace MaintenanceAPI_1
{
    using System;

    using Skyline.DataMiner.Automation;
    using Skyline.DataMiner.Net.Apps.UserDefinableApis;
    using Skyline.DataMiner.Net.Apps.UserDefinableApis.Actions;

    public class Script
	{
        [AutomationEntryPoint(AutomationEntryPointType.Types.OnApiTrigger)]
        public ApiTriggerOutput OnApiTrigger(IEngine engine, ApiTriggerInput requestData)
        {
            if (requestData.RawBody.Equals("start", StringComparison.InvariantCultureIgnoreCase))
            {
                Start(engine);

                return new ApiTriggerOutput
                {
                    ResponseBody = "Maintenance started. Masking the device.",
                    ResponseCode = (int)StatusCode.Ok,
                };
            }

            if (requestData.RawBody.Equals("stop", StringComparison.InvariantCultureIgnoreCase))
            {
                Stop(engine);

                return new ApiTriggerOutput
                {
                    ResponseBody = "Maintenance stopped. Unmasking the device.",
                    ResponseCode = (int)StatusCode.Ok,
                };
            }

            return new ApiTriggerOutput
            {
                ResponseBody = "Wrong input",
                ResponseCode = (int)StatusCode.BadRequest,
            };
        }

        public void Start(IEngine engine)
        {
            var encoderDecoder = engine.FindElement("EncoderDecoder");
            encoderDecoder.Mask("Under maintenance.");
        }

        public void Stop(IEngine engine)
        {
            var encoderDecoder = engine.FindElement("EncoderDecoder");
            encoderDecoder.Unmask();
        }
    }
}