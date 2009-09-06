using System;
using Mike.NHibernateDemo.Model;

namespace Mike.NHibernateDemo
{
    public static class ModelExtensions
    {
        public static bool IsEntity(this Type type)
        {
            return typeof (IEntity).IsAssignableFrom(type);
        }

        public static bool IsComponent(this Type type)
        {
            return typeof (IComponent).IsAssignableFrom(type);
        }
    }
}