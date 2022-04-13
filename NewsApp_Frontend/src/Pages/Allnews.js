import React,{Component} from "react";
import { variables } from '../Variables';
export default class Allnews extends Component{
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
    render(){
        return(
            <div>
                Allnews!
            </div>
        )
    }
}