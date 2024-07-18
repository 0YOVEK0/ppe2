using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GoSystem
{
    [System.AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
    public class GBehaviourAttributeAttribute : PropertyAttribute
    {
        public string tittle;
        public string icon;
        public bool useOpenClose;
        /// <summary>
        /// Create a header for your inspector
        /// </summary>
        /// <param name="tittle">Title of the header</param>
        /// <param name="icon">Icon of the header (this is a name of the icon inside the Resources folder)</param>
        /// <param name="useOpenClose">Use Open Close Inpector button</param>
        public GBehaviourAttributeAttribute(string tittle="",bool useOpenClose=true)
        {
            string icon = "LO";
            this.tittle = tittle;
            this.icon = icon;
            this.useOpenClose = useOpenClose;

        }
    }
}