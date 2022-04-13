import React,{Component} from "react";
import { Card, Container, Row, Image } from "react-bootstrap";
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

    render() {
        const {
            news
        }=this.state;
        return (
            <>
            {news.map(n =>
            <Container fluid="md" key={n.NewsID}>
                <Row >
                <Image src={variables.FILES_URL+n.NewsImagePath}/>
                <Card style={{ width: '18rem' }}>
                    <Card.Body>
                        <Card.Title>{n.NewsHeader}</Card.Title>
                        <Card.Text>{n.NewsSubtitle}</Card.Text>
                    </Card.Body>
                </Card>
                </Row>
            </Container>
            )}
            </>
        );
    }
}