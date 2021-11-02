<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Automatas.aspx.cs" Inherits="Automatas.Automatas" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous"/>
    <script src="https://code.jquery.com/jquery-3.4.1.slim.min.js" integrity="sha384-J6qa4849blE2+poT4WnyKhv5vZF5SrPo0iEjwBvKU7imGFAV0wwj1yYfoRSJoZ+n" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js" integrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6" crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" integrity="sha384-1BmE4kWBq78iYhFldvKuhfTAU6auU8tT94WrHftjDbrCEXSU1oBoqyl2QvZ6jIW3" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.min.js" integrity="sha384-QJHtvGhmr9XOIpI6YVutG+2QOK9T+ZnN4kzFN1RtK3zEFEIsxhlmWl5/YESvpZ13" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-ka7Sk0Gln4gmtz2MlQnikT1wXgYsOg+OMhuP+IlRH9sENBO0LRn5q+8nbTov4+1p" crossorigin="anonymous"></script>
</head>
<body>
    <nav class="navbar navbar-dark bg-dark">
        
          <a class="navbar-brand" href="#">AUTOMATAS</a>
        <ul class="nav navbar-nav">
          
        </ul>
      
    </nav>
    <div id="main">
        <section class="container">
            <div class="container">
                <form id="form1" runat="server">
                    <br />
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="custom-file">
                                <input type="file" class="custom-file-input" id="file" lang="es" runat="server"/>
                                <label class="custom-file-label" for="customFileLang">Seleccionar Archivo</label>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <button type="button" class="btn btn-success" runat="server" onserverclick="cargar">Cargar archivo</button>
                        </div>
                    </div>
                    <br />
                    
                    <div class="row" id="divuno" runat="server" visible="false">
                        <div class="col-sm-3">
                            <div class="md-form">
                                <i class="fa fa-file-o"></i>
                                <label>TXT</label>
                                <textarea id="areaArchivo" runat="server" class="md-textarea form-control" rows="10" readonly="readonly" style="resize: none;"></textarea>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="md-form">
                                <i class="fa fa-quora"></i>
                                <label>Q</label>
                                <textarea id="areaQEstados" runat="server" class="md-textarea form-control" rows="10" readonly="readonly" style="resize: none;"></textarea>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="md-form">
                                <i class="fa fa-facebook"></i>
                                <label>Σ</label>
                                <textarea id="areaFAlfabeto" runat="server" class="md-textarea form-control" rows="10" readonly="readonly" style="resize: none;"></textarea>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="md-form">
                                <i class="fa fa-font"></i>
                                <label>A</label>
                                <textarea id="areaAAceptacion" runat="server" class="md-textarea form-control" rows="10" readonly="readonly" style="resize: none;"></textarea>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="md-form">
                                <i class="fa fa-wikipedia-w"></i>
                                <label>W = AFN</label>
                                <asp:Panel ID="panel1" runat="server" Height="285px" Width="100%" ScrollBars="Vertical">
                                    <asp:GridView ID="GridViewAFN" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="True" DataKeyNames="N" EmptyDataText="There are no data records to display.">
                                    <%--<HeaderStyle CssClass="thead-custom"/>--%>
                                </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divdos" runat="server" visible="false">
                        <div class="col-sm-5">
                            <div class="md-form">
                                <i class="fa fa-wikipedia-w"></i>
                                <label>W = AFD</label>
                                <asp:Panel ID="panel2" runat="server" Height="285px" ScrollBars="Vertical">
                                    <asp:GridView ID="GridViewAFD" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="True" DataKeyNames="ESTADO" EmptyDataText="There are no data records to display.">
                                        <HeaderStyle CssClass="thead-custom"/>
                                    </asp:GridView>
                                    <%--<asp:GridView ID="GridViewAFD" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="True" DataKeyNames="N" EmptyDataText="There are no data records to display.">
                                        <HeaderStyle CssClass="thead-custom"/>
                                    </asp:GridView>--%>
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="md-form">
                                <i class="fa fa-file-o"></i>
                                <label>Quintupla</label>
                                <textarea id="taQuintupla" runat="server" class="md-textarea form-control" rows="10" readonly="readonly"></textarea>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="md-form">
                                <asp:TextBox ID="tbxCadena" runat="server" CssClass="form-control input-lg" Width="100%" TabIndex="1" placeholder="Cadena"/>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="md-form">
                                <label>.</label>
                                <button type="button" class="btn btn-warning" id="btnValidarCadena" runat="server" onserverclick="btnValidarCadena_Click">Validar cadena</button>
                            </div>
                        </div>
                    </div>

                </form>
            </div>
        </section>
    </div>
</body>
</html>
