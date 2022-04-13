import 'bootstrap/dist/css/bootstrap.min.css';
import React,{Component} from "react";
import { variables } from '../Variables';

export default class CarouselBox extends Component{
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
            <div id="carouselExampleControls" className="carousel slide" data-bs-ride="carousel">
            {news.map(n =>
                <div className="carousel-inner" key={n.NewsID}>
                <div className="carousel-item active" >
                <img 
                className="d-block w-100"
                src={variables.FILES_URL+n.NewsItemPath}
                alt={n.NewsName}/>
                <div className="carousel-caption d-none d-md-block">
                    <h3>{n.NewsHeader}</h3>
                </div>
                </div>
                </div>
                )}
                <button className="carousel-control-prev" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="prev">
                    <span className="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span className="visually-hidden">Previous</span>
                </button>
                <button className="carousel-control-next" type="button" data-bs-target="#carouselExampleCaptions" data-bs-slide="next">
                    <span className="carousel-control-next-icon" aria-hidden="true"></span>
                    <span className="visually-hidden">Next</span>
                </button>
            </div>
        );
    }
}