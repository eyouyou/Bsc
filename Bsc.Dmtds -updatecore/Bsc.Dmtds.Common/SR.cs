using System.Collections.Specialized;

namespace Bsc.Dmtds.Common
{
    /// <summary>
    /// 系统资源
    /// </summary>
    public class SR
    {
        public static NameValueCollection resources = new NameValueCollection();
        static SR()
        {
            resources["RequiredAttribute_ValidationError"] = "{0} 字段是必须的.";
            resources["StringLengthAttribute_ValidationError"] = "{0} 字段字符串长度最大为 {1}.";
            resources["RangeAttribute_ValidationError"] = "{0} 字段在范围 {1} 和 {2}之间";
            resources["RegexAttribute_ValidationError"] = "{0} 字段必须符合正则表达式： '{1}'.";
            resources["ClientDataTypeModelValidatorProvider_FieldMustBeNumeric"] = "{0} 字段必须为数字.";
            resources["RangeAttribute_MinGreaterThanMax"] = "最大值： '{0}' 必须大于最小值： '{1}'.";
            resources["RangeAttribute_Must_Set_Min_And_Max"] = "必须指定最大值和最小值.";
            resources["RangeAttribute_Must_Set_Operand_Type"] = "必须指定操作数类型（当字符串有最大最小值）.";
            resources["RangeAttribute_ArbitraryTypeNotIComparable"] = "类型 {0} 必须实现 {1}.";

            //System_Web_Mvc_Resources
            resources["Common_ValueNotValidForProperty"] = "值： '{0}' 无效.";
            resources["ClientDataTypeModelValidatorProvider_FieldMustBeNumeric"] = "{0} 字段必须是一个数字.";
            resources["ViewPageHttpHandlerWrapper_ExceptionOccurred"] = "执行子请求失败. 请测试内部异常.";
            resources["RedirectAction_CannotRedirectInChildAction"] = "子action不允许重定向.";
            resources["Common_NoRouteMatched"] = "没有路由记录匹配.";
            resources["RemoteAttribute_NoUrlFound"] = "请求的远程验证不存在.";
            resources["Controller_UnknownAction"] = "一个 public action 方法 '{0}' 在控制器 '{1}'中不存在.";
            resources["Common_NullOrEmpty"] = "值不能为空.";

            //System_Web_Resources
            resources["Webevent_event_custom_event_details"] = "自定义事件详情: ";
            resources["Path_not_found"] = "路径 '{0}' 不存在.";
            resources["Webevent_event_request_url"] = "请求 URL: {0}";
            resources["Webevent_event_request_path"] = "请求路径: {0}";
            resources["Webevent_event_user_host_address"] = "用户主机地址: {0}";
            resources["Webevent_event_user"] = "用户: {0}";
            resources["Webevent_event_is_authenticated"] = "是否通过身份验证: 是";
            resources["Webevent_event_is_not_authenticated"] = "是否通过身份验证: 否";
            resources["Webevent_event_authentication_type"] = "身份验证方式: {0}";
            resources["Webevent_event_thread_account_name"] = "线程 用户名: {0}";
            resources["Webevent_event_thread_id"] = "线程 ID: {0}";
            resources["Webevent_event_is_impersonating"] = "是否冒充: 是";
            resources["Webevent_event_is_not_impersonating"] = "是否冒充: 否";
            resources["Webevent_event_stack_trace"] = "跟踪栈: {0}";
            resources["Webevent_event_application_domain"] = "应用程序域: {0}";
            resources["Webevent_event_trust_level"] = "信任等级: {0}";
            resources["Webevent_event_application_virtual_path"] = "应用程序虚拟路径: {0}";
            resources["Webevent_event_application_path"] = "应用程序路径: {0}";
            resources["Webevent_event_machine_name"] = "机器名: {0}";
            resources["Webevent_event_code"] = "事件代码: {0}";
            resources["Webevent_event_message"] = "事件信息: {0}";
            resources["Webevent_event_time"] = "事件事件: {0}";
            resources["Webevent_event_time_Utc"] = "事件事件 (UTC): {0}";
            resources["Webevent_event_id"] = "事件 ID: {0}";
            resources["Webevent_event_sequence"] = "事件序列: {0}";
            resources["Webevent_event_detail_code"] = "事件详细代码: {0}";
            resources["Webevent_event_application_information"] = "应用程序信息:";
            resources["Webevent_event_exception_information"] = "异常信息:";
            resources["Webevent_event_inner_exception_information"] = "内部异常信息 (等级 {0}):";
            resources["Webevent_event_exception_type"] = "异常类型: {0}";
            resources["Webevent_event_exception_message"] = "异常信息: {0}";
            resources["Webevent_event_request_information"] = "请求信息:";
            resources["Webevent_event_thread_information"] = "线程信息:";


            //System_Linq_Resources
            resources["NoElements"] = "序列不包含任何元素";
        }
        public static string GetString(string key)
        {
            string s = resources[key];
            if (string.IsNullOrEmpty(s))
            {
                s = key;
            }
            return s;
        }
    }
}