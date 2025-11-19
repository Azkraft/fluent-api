using System.Globalization;
using System.Reflection;

namespace Homework;

public class PropertyPrintingConfig<TOwner, TPropType>(PrintingConfig<TOwner> printingConfig, MemberInfo? memberInfo)
    : IPropertyPrintingConfig<TOwner, TPropType>
{
    public PrintingConfig<TOwner> Using(Func<TPropType, string> print)
    {
        if (memberInfo is not null)
        {
            ((IPrintingConfig)printingConfig).AlternativeMembersSerialization[memberInfo] = obj => print((TPropType)obj);
        }
        else
        {
            ((IPrintingConfig)printingConfig).AlternativeTypesSerialization[typeof(TPropType)] = obj => print((TPropType)obj);
        }

        return printingConfig;
    }

    public PrintingConfig<TOwner> Using(CultureInfo culture)
    {
        if (typeof(TPropType).GetMethod("ToString", [typeof(IFormatProvider)]) is null)
            throw new Exception();

        ((IPrintingConfig)printingConfig).TypesCultureInfo[typeof(TPropType)] = culture;

        return printingConfig;
    }

    PrintingConfig<TOwner> IPropertyPrintingConfig<TOwner, TPropType>.ParentConfig => printingConfig;
    MemberInfo? IPropertyPrintingConfig<TOwner, TPropType>.MemberInfo => memberInfo;
}
