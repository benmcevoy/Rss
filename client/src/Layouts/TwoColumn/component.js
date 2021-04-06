export default function TwoColumn(props) {
    return (<div className="App">
        <header className="App-header">
            <div class="container">
                <div class="row">
                    {props.header}
                </div>
            </div>
        </header>
        <div class="container">
            <div class="row">
                <aside className="App-aside col">
                    {props.aside}
                </aside>
                <main className="App-main col-8">
                    {props.main}
                </main>
            </div>
        </div>
    </div>);
}