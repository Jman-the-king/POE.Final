<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="POE.Final._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<!DOCTYPE html>

<html>

<body>

<form id="form1" enctype="multipart/form-data">

<div>

<label for="Id">ID (for Update/Delete):</label> <br />

<asp:TextBox ID="IdTextBox" runat="server"></asp:TextBox>

<br />


<label for="Lastname">Lastname:</label><br/>

<asp:TextBox ID="LastNameTextBox" runat="server"></asp:TextBox>

<br />


<label for="HoursWorked">Hours Worked:</label><br/>

<asp:TextBox ID="HoursWorkedTextBox" runat="server"></asp:TextBox>

<br />


<label for="HourlyRate">Hourly Rate:</label><br/>

<asp:TextBox ID="HourlyRateTextBox" runat="server"></asp:TextBox>

<br />

<label for="Salary">Salary:</label><br />

<asp:TextBox ID="SalaryTextBox" runat="server" ReadOnly="true"></asp:TextBox>

<br />

<%--<label for="Status">Status:</label><br />

<asp:TextBox ID="StatusTextBox" runat="server" ReadOnly="true"></asp:TextBox>

<br />--%>

    <!-- Content visible only after submitting -->
<asp:Panel ID="ResultPanel" runat="server" Visible="false">
    <label for="Salary">Salary:</label><br />
    <asp:TextBox ID="TextBox1" runat="server" ReadOnly="true"></asp:TextBox>
    <br />
</asp:Panel>





<label for="AdditionalNotes">Upload File:</label>

<asp:FileUpload ID="FileUploadControl" runat="server" />

<br />


<asp:Button ID="SubmitButton" runat="server" Text="Add" OnClick="SubmitButton_Click" />

<br /><br />



<asp:GridView ID="LecturerGridView" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="EmployeesGridView_SelectedIndexChanged">

<Columns>

        <asp:BoundField DataField="Id" HeaderText="ID" />

        <asp:BoundField DataField="Surname" HeaderText="Surname" />

        <asp:BoundField DataField="HoursWorked" HeaderText="Hours Worked" />

        <asp:BoundField DataField="HourlyRate" HeaderText="Hourly Rate" />

        <asp:BoundField DataField="Salary" HeaderText="Salary" />

        <asp:BoundField DataField="Status" HeaderText="Status" />

        <asp:BoundField DataField="FilePath" HeaderText="Uploaded File" />

        <asp:CommandField ShowSelectButton="True" />

</Columns>

</asp:GridView>

</div>

</form>

</body>

</html>
</asp:Content>

