using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RoleSample.Models
{
    public class MenuItemModel
    {
        /// <summary>
        /// menu代號
        /// </summary>
        [Required(ErrorMessage = "menu代號不可空白")]
        public int Id { get; set; }

        /// <summary>
        /// parent代號 (0:Root)
        /// </summary>
        [Required(ErrorMessage = "parent代號不可空白")]
        public int Parent { get; set; }

        /// <summary>
        /// Display_Order 
        /// </summary>
        [Required(ErrorMessage = "次序不可空白")]
        public int Order { get; set; }

        /// <summary>
        /// 選單名稱
        /// </summary>
        [Required(ErrorMessage = "選單名稱不可空白")]
        public string Name { get; set; }

        /// <summary>
        /// 角色名稱
        /// </summary>
        [Required(ErrorMessage = "角色名稱不可空白")]
        public string RoleName { get; set; }

        /// <summary>
        /// Controller 名稱
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Action 名稱
        /// </summary>
        public string Action { get; set; }
    }
}