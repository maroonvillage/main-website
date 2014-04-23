﻿using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;

namespace MainWebsite.Helpers
{
    public sealed class SessionHelper : DynamicObject
    {
        private static readonly SessionHelper sessionBag;

        static SessionHelper()
        {
            sessionBag = new SessionHelper();
        }

        private SessionHelper() { }


        public static dynamic Current
        {
            get { return sessionBag; }
        }

        private HttpSessionStateBase Session
        {
            get
            {
                return new HttpSessionStateWrapper(HttpContext.Current.Session);
            }

        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = Session[binder.Name];
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            Session[binder.Name] = value;
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            int index = (int)indexes[0];
            result = Session[index];
            return result != null;
        }

        public override bool TrySetIndex(SetIndexBinder binder,
               object[] indexes, object value)
        {
            int index = (int)indexes[0];
            Session[index] = value;
            return true;
        }
    }
}