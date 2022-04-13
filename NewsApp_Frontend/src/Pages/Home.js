import { Carousel, Item, Cap } from "bootstrap";
import React,{Component} from "react";
import { variables } from '../Variables';

export default class Home extends Component{
    constructor(props) {
        super(props);
        this.state = { news: []};
    }

    refreshList(){
        fetch(variables.API_URL+'home')
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
            <>
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <tbody>
                    {news.map(n =>
                        <tr key={n.NewsID}>
                            <td><img src={variables.FILES_URL+n.NewsImagePath}/></td>
                            <td>{n.NewsHeader}</td>
                            <td>{n.NewsSubtitle}</td>
                        </tr>
                    )}
                </tbody>
            </table>
            </>
        );
    }
}