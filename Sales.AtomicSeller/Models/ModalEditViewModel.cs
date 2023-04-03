namespace Sales.AtomicSeller.Models
{
    public class ModalEditViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string LoaderId { get; set; }
        public string FormAction { get; set; } = "Edit";
        public string FormController { get; set; }
        public string FormArea { get; set; }
        public string FormDataAjaxSuccess { get; set; }
        public string FormDataAjaxFailure { get; set; }
    }
}