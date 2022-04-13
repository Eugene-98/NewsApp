import React,{Component} from "react";
import { Row, Table, Form, Group, Label, Control, Feedback, Col } from "react-bootstrap";
import 'bootstrap/dist/css/bootstrap.min.css';
import { variables } from '../Variables';

export default class AdminPage extends Component{
    constructor(props) {
    super(props);
        this.state = { 
            news: [],
            modalTitle:"",
            NewsName:"",
            NewsHeader:"",
            NewsSubtitle:"",
            NewsImageName:"noImage.png",
            NewsImagePath:variables.FILES_URL,
            NewsText:"",
            NevsId:0
    };
    }
    refreshList(){
        fetch(variables.API_URL+'news')
        .then(response=>response.json())
        .then(data=>{
            this.setState({news:data});
    });
    }
    componentDidMount() {
        this.refreshList();
    }
    changeNewsName =(e)=>{
        this.setState({DepartmentName:e.target.value});
    }
    changeNewsHeader =(e)=>{
        this.setState({DepartmentName:e.target.value});
    }
    changeNewsSubtitle =(e)=>{
        this.setState({DepartmentName:e.target.value});
    }
    changeNewsText =(e)=>{
        this.setState({DepartmentName:e.target.value});
    }
    changeNewsImage =(e)=>{
        this.setState({DepartmentName:e.target.value});
    }
    
    addClick(){
        this.setState({
            modalTitle:"Add News",
            NewsId:0,
            NewsName:"",
            NewsHeader:"",
            NewsSubtitle:"",
            NewsImageName:"",
            NewsImagePath:"",
            NewsText:""
        });
    }
    editClick(ne){
        this.setState({
            modalTitle:"Edit News",
            NewsId:ne.NewsId,
            NewsName:ne.NewsName,
            NewsHeader:ne.NewsHeader,
            NewsSubtitle:ne.NewsSubtitle,
            NewsImageName:ne.NewsImageName,
            NewsImagePath:ne.NewsImagePath,
            NewsText:ne.NewsText
        });
    }
    createClick(){
        fetch(variables.API_URL+'news',{
            method:'POST',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                NewsName:this.state.NewsName,
                NewsHeader:this.state.NewsHeader,
                NewsSubtitle:this.state.NewsSubtitle,
                NewsText:this.state.NewsText
            })
        })
        .then(res=>res.json())
        .then((result)=>{
            alert(result);
            this.refreshList();
        },(error)=>{
            alert('Failed')
        })
    }
    render(){
    const {
        news,
        modalTitle,
        NewsId,
        NewsName,
        NewsHeader,
        NewsSubtitle,
        NewsImageName,
        NewsImagePath,
        NewsText,


    }=this.state;
    return(
    <>
        <button type="button" 
        className="btn btn-primary m-2 float-end"
        data-bs-toggle="modal"
        data-bs-target="#exampleModal"
        onClick={()=>this.addClick()}>
            Add News
        </button>
        <Table striped bordered hover>
            <thead>
                <tr>
                    <th>News Id</th>
                    <th>News Name</th>
                    <th>News Header</th>
                    <th>News Subtitle</th>
                    <th>News Image Name</th>
                    <th>News Image Path</th>
                    <th>News Text</th>
                </tr>
            </thead>
            <tbody >
                {news.map(ne=>
                    <tr key={ne.NewsId}>
                        <td>{ne.NewsId}</td>
                        <td>{ne.NewsName}</td>
                        <td>{ne.NewsHeader}</td>
                        <td>{ne.NewsSubtitle}</td>
                        <td>{ne.NewsImageName}</td>
                        <td>{ne.NewsImagePath}</td>
                        <td>{ne.NewsText}</td>
                        <td>
                            <button type="button"
                            className="btn btn-light mr-1"
                            data-bs-toggle="modal"
                            data-bs-target="#exampleModal"
                            onClick={()=>this.editClick(ne)}>
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-pencil-square" viewBox="0 0 16 16">
                                <path d="M15.502 1.94a.5.5 0 0 1 0 .706L14.459 3.69l-2-2L13.502.646a.5.5 0 0 1 .707 0l1.293 1.293zm-1.75 2.456-2-2L4.939 9.21a.5.5 0 0 0-.121.196l-.805 2.414a.25.25 0 0 0 .316.316l2.414-.805a.5.5 0 0 0 .196-.12l6.813-6.814z"/>
                                <path fillRule="evenodd" d="M1 13.5A1.5 1.5 0 0 0 2.5 15h11a1.5 1.5 0 0 0 1.5-1.5v-6a.5.5 0 0 0-1 0v6a.5.5 0 0 1-.5.5h-11a.5.5 0 0 1-.5-.5v-11a.5.5 0 0 1 .5-.5H9a.5.5 0 0 0 0-1H2.5A1.5 1.5 0 0 0 1 2.5v11z"/>
                                </svg>
                            </button>
                            <button type="button"
                            className="btn btn-light mr-1">
                                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" className="bi bi-trash" viewBox="0 0 16 16">
                                <path d="M5.5 5.5A.5.5 0 0 1 6 6v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm2.5 0a.5.5 0 0 1 .5.5v6a.5.5 0 0 1-1 0V6a.5.5 0 0 1 .5-.5zm3 .5a.5.5 0 0 0-1 0v6a.5.5 0 0 0 1 0V6z"/>
                                <path fillRule="evenodd" d="M14.5 3a1 1 0 0 1-1 1H13v9a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2V4h-.5a1 1 0 0 1-1-1V2a1 1 0 0 1 1-1H6a1 1 0 0 1 1-1h2a1 1 0 0 1 1 1h3.5a1 1 0 0 1 1 1v1zM4.118 4 4 4.059V13a1 1 0 0 0 1 1h6a1 1 0 0 0 1-1V4.059L11.882 4H4.118zM2.5 3V2h11v1h-11z"/>
                                </svg>
                            </button>
                        </td>
                    </tr>
                )}
            </tbody>
        </Table>
    <div className="modal fade" id="exampleModal" tabIndex="-1" aria-hidden="true">
    <div className="modal-dialog modal-lg modal-dialog-centered">
    <div className="modal-content">
        <div className="modal-header">
            <h5 className="modal-title">{modalTitle}</h5>
            <button type="button" className="btn-close" data-bs-dismiss="modal"aria-label="Close"></button>
        </div>

        <Form>
          <Row>
            <Form.Group
              as={Col}
              md="4"
              controlId="validationFormik101"
              className="position-relative"
            >
              <Form.Label>News Name</Form.Label>
              <Form.Control
                type="text"
                name="newsName"
                value={NewsName}
                onChange={this.changeNewsName}
              />
              <Form.Control.Feedback tooltip>Ok</Form.Control.Feedback>
            </Form.Group>
          </Row>
          <Row>
            <Form.Group
              as={Col}
              md="4"
              controlId="validationFormik102"
              className="position-relative">
              <Form.Label>NewsHeader</Form.Label>
              <Form.Control
                type="text"
                name="newsHeader"
                value={NewsHeader}
                onChange={this.changeNewsHeader}/>
            </Form.Group>
          </Row>
          <Row>
          <Form.Group
              as={Col}
              md="4"
              controlId="validationFormik102"
              className="position-relative">
              <Form.Label>NewsHeader</Form.Label>
              <Form.Control
                type="text"
                name="newsHeader"
                value={NewsSubtitle}
                onChange={this.changeNewsSubtitle}/>
            </Form.Group>
          </Row>
          <Row>
            <Form.Group
              as={Col}
              md="6"
              controlId="validationFormik103"
              className="position-relative"
            >
              <Form.Label>News Text</Form.Label>
              <Form.Control
                type="text"
                placeholder="Text"
                name="city"
                value={NewsText}
                onChange={this.changeNewsText}/>
            </Form.Group>
          </Row>
          <Row>
          <Form.Group className="position-relative mb-3">
            <Form.Label>File</Form.Label>
            <Form.Control
              type="file"
              required
              name="file"
              onChange={this.changeNewsImage}
            />
          </Form.Group>
          <div className="p-2 w-50 bd-highlight">
          <img width="250px" height="250px"
          src={NewsImagePath+NewsImageName}/>
         <input className="m-2" type="file" onChange={this.imageUpload}/>
     </div>
          </Row>
          <Row>
            {NewsId==0?
            <button type="button"
            className="btn btn-primary float-start"
            onClick={()=>this.createClick()}>
                Create
            </button>
            :null}

            {NewsId!=0?
            <button type="button"
            className="btn btn-primary float-start">
                Update
            </button>
            :null}
            </Row>
        </Form>
    </div>
    </div>
    </div>
    </>
        )
    }
}
