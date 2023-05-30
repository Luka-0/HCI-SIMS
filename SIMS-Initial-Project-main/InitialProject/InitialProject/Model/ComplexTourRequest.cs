using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.ComTypes;
using InitialProject.Enumeration;

namespace InitialProject.Model;

[Table("ComplexTourRequest")]

public class ComplexTourRequest
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public List<TourRequest>? Requests { get; set; }
    public TourRequestState State { get; set; }

    public ComplexTourRequest()
    {
        State = TourRequestState.Pending;
    }

}