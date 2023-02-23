namespace BlazorTimeEntryStatusModule.Models;

public class EmployeeResponse
{
    public List<Employee> Employees { get; set; }

    // NOTE: An /Employees/Me/ call will not include the following properties in its result
    public string PrevPageURI { get; set; }
    public string NextPageURI { get; set; }
}

public class Employee
{
    // If the logged in user doesn't have permission to call /Employees/, we call /Employees/Me/ instead. Unfortunately, that call doesn't return a
    // WorkDays value so we need to make sure the _WorkDaysArray is populated with a default value.
    public Employee() { SetWorkDays(_WorkDays); }

    public string ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    // Indicates which days the employee works in the following the format (can be different for each employee): .MTWTF.
    protected string _WorkDays = ".MTWTF.";
    public string WorkDays
    {
        get { return _WorkDays; }
        set { SetWorkDays(value); }
    }


    protected List<bool> _WorkDaysArray = new List<bool>();
    protected void SetWorkDays(string WorkDays)
    {
        // Remember the original string
        _WorkDays = WorkDays;

        // Create the WorkDays array. Each day that is not a dot is a workday.
        _WorkDaysArray.Clear();
        foreach (char Day in WorkDays) { _WorkDaysArray.Add((Day != '.')); }
    }

    // DayOfWeek is a zero-based value starting with Sunday
    public bool IsWorkingDay(DayOfWeek Day) { return _WorkDaysArray[(int)Day]; }
}

