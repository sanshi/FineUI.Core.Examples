// 用来归组共享资源的占位类

namespace FineUI.Core.Examples
{


    #region ParamsFunc Delegate

    // https://stackoverflow.com/questions/27565950/passing-params-through-a-func
    /// <summary>
    /// 封装一个包含 params 参数的方法，并返回TResult类型的值。
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    /// <param name="arg1"></param>
    /// <param name="arg2"></param>
    /// <returns></returns>
    public delegate TResult ParamsFunc<T1, T2, TResult>(T1 arg1, params T2[] arg2);

    #endregion


    /// <summary>
    /// 共享的数据注解资源类
    /// </summary>
    public class SharedAnnotationResources
    {
    }


    /// <summary>
    /// 共享的JavaScript资源类
    /// </summary>
    public class SharedJavaScriptResources
    {
    }

    /// <summary>
    /// 共享的资源类
    /// </summary>
    public class SharedResources
    {
    }

}