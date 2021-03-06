using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.Models.Extensions
{
    public static class ControllerExtensions
    {
		public static void AddFlashMessage(this Controller controller, FlashMessage message)
		{
			List<FlashMessage> list = controller.TempData.DeserializeToObject<List<FlashMessage>>("Messages");

			list.Add(message);
			controller.TempData.SerializeObject(list, "Messages");
		}

		public static void AddFlashMessage(this Controller controller, string message, FlashMessageType messageType)
		{
			controller.AddFlashMessage(new FlashMessage(message, messageType));
		}
	}
}
