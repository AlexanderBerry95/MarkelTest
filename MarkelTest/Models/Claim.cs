namespace MarkelTest.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public string UCR { get; set; }
        public int CompanyId { get; set; }
        public DateTime ClaimDate { get; set; }
        public DateTime LossDate { get; set; }
        public string AssuredName { get; set; }
        public decimal IncurredLoss { get; set; }
        public bool Closed { get; set; }
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - ClaimDate.Year;

                if (ClaimDate.Date > today.AddYears(-age))
                {
                    age--;
                }

                return age;
            }
        }
    }

}
