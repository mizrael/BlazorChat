using System.ComponentModel.DataAnnotations;

namespace BlazorChat.ViewModels
{

    public class Message{

        [Required, MinLength(1)]
        public string Text{get;set;}
    }
}
