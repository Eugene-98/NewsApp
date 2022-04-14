
import React,{Component} from "react";
import { Outlet} from "react-router-dom";
import { variables } from '../Variables';



export default class News extends Component{
    constructor(props) {
        super(props);
        this.state = { news: []};
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

    render() {
        const {
            news
        }=this.state;
        return (
            <Outlet context={news}/>
        );
    }
}