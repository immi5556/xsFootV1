 
// Type: app.bsms.Helpers.NullToEmptyStringResolver
 
 
 

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace app.bsms.Helpers
{
    //  public class NullToEmptyStringResolver : DefaultContractResolver
    //  {
    //    protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
    //    {
    //      return (IList<JsonProperty>) ((IEnumerable<PropertyInfo>) type.GetProperties()).Select<PropertyInfo, JsonProperty>((Func<PropertyInfo, JsonProperty>) (p =>
    //      {
    //        // ISSUE: reference to a compiler-generated method
    //        JsonProperty jsonProperty = this.\u003C\u003En__0((MemberInfo) p, memberSerialization);
    //        jsonProperty.ValueProvider = (IValueProvider) new NullToEmptyStringValueProvider(p);
    //        return jsonProperty;
    //      })).ToList<JsonProperty>();
    //    }
    //  }

    //public class NullToEmptyStringResolver : DefaultContractResolver
    //{
    //    public NullToEmptyStringResolver()
    //    {
    //    }

//        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
  //      {
   //         return type.GetProperties().Select<PropertyInfo, JsonProperty>((PropertyInfo p) =>
   //         {
   //             JsonProperty nullToEmptyStringValueProvider = this.model(p, memberSerialization);
   //             nullToEmptyStringValueProvider.ValueProvider = new NullToEmptyStringValueProvider(p);
   //             return nullToEmptyStringValueProvider;
   //         }).ToList<JsonProperty>();
   //     }
   // }

        public class NullToEmptyStringResolver : DefaultContractResolver
        {
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                return type.GetProperties()
                .Select(p => {
                    var jp = base.CreateProperty(p, memberSerialization);
                    jp.ValueProvider = new EmptyToNullStringValueProvider(p);
                    return jp;
                }).ToList<JsonProperty>();
            }
        }


    public class EmptyToNullStringValueProvider : IValueProvider
    {
        PropertyInfo _MemberInfo;

        public EmptyToNullStringValueProvider(PropertyInfo memberInfo)
        {
            _MemberInfo = memberInfo;
        }

        public object GetValue(object target)
        {
            object result = _MemberInfo.GetValue(target);

            if (_MemberInfo.PropertyType == typeof(string) && result != null && string.IsNullOrWhiteSpace(result.ToString()))
            {
                result = null;
            }

            return result;
        }

        public void SetValue(object target, object value)
        {
            if (_MemberInfo.PropertyType == typeof(string) && value != null && string.IsNullOrWhiteSpace(value.ToString()))
            {
                value = null;
            }

            _MemberInfo.SetValue(target, value);
        }
    }

}
