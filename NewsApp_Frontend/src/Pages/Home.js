import { Carousel } from "bootstrap";
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
                <thead>
                    <tr>
                        <th>Header</th>
                        <th>Subtitle</th>
                        <th>Text</th>
                    </tr>
                </thead>
                <tbody>
                    {news.map(n =>
                        <tr key={n.NewsID}>
                            <td>{n.NewsHeader}</td>
                            <td>{n.NewsSubtitle}</td>
                            <td>{n.NewsText}</td>
                            <td><img src={n.NewsImagePath}/></td>
                        </tr>
                    )}
                </tbody>
            </table>
            </>
        );
    }
}