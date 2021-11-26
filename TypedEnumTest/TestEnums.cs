// A collection of random enum values for testing.
namespace TypedEnumTest
{
    public enum NUnitEnum
    {
        NoValue = 0x0000,
        PropertiesChanged = 0x0001,
        GotDesignerEventService = 0x0002,
        InternalChange = 0x0004,
        TabsChanging = 0x0008,
        BatchMode = 0x0010,
        ReInitTab = 0x0020,
        SysColorChangeRefresh = 0x0040,
        FullRefreshAfterBatch = 0x0080,
        BatchModeChange = 0x0100,
        RefreshingProperties = 0x0200
    }

    public enum InvalidEnum
    {
        Value1 = 1,
        Value2 = 2,
        Value3 = 3
    }

    public enum EnumByte : byte
    {
        Value1,
        Value2,
        Value3
    }

    public enum EnumSByte : sbyte
    {
        Value1,
        Value2,
        Value3
    }

    public enum EnumUShort : ushort
    {
        Value1,
        Value2,
        Value3
    }

    public enum EnumShort : short
    {
        Value1,
        Value2,
        Value3
    }

    public enum EnumUInt : uint
    {
        Value1,
        Value2,
        Value3
    }

    public enum EnumInt : int
    {
        Value1,
        Value2,
        Value3
    }

    public enum EnumULong : ulong
    {
        Value1,
        Value2,
        Value3
    }

    public enum EnumLong : long
    {
        Value1,
        Value2,
        Value3
    }
}
