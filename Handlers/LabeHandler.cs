
namespace OrderLookup.Handlers
{
    public class CustomLabel : Label
    {
        public CustomLabel()
        {
            Microsoft.Maui.Handlers.LabelHandler.Mapper.AppendToMapping("MyLblCustomization", (handler, view) =>
            {
                if (view is CustomLabel)
                {
#if WINDOWS
                    handler.PlatformView.IsTextSelectionEnabled = true;
#endif                
                }
            });
        }
    }
}
