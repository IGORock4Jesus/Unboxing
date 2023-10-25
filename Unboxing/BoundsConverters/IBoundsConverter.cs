namespace Unboxing.BoundsConverters;
internal interface IBoundsConverter
{
    float Convert(string value);
    bool Can(string value);
}
