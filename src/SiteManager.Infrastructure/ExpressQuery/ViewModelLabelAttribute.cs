using SiteManager.EnumLibrary;

namespace SiteManager.Infrastructure.ExpressQuery
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ViewModelLabelAttribute:Attribute
    {
        public ExpressComparison Comparison { get; private set; }

        /// <summary>
        /// 生成表达式树，需要比较的方式
        /// </summary>
        /// <param name="expressComparison">比较方式</param>
        public ViewModelLabelAttribute(ExpressComparison expressComparison)
        {
            Comparison = expressComparison;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class ViewModelTypeLabelAttribute : Attribute
    {
        public bool IsOpenPage { get; set; } = false;
    }
}