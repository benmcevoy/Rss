export default function TwoColumn(props) {
    return (<div className="App">
        <header className="App-header">
            <div class="container">
                <div class="row">
                    {props.header}
                </div>
            </div>
        </header>
        <div class="row">
            <div class="container">
                <aside className="App-aside col-sm-4">
                    {props.aside}
                </aside>
                <main className="App-main col-sm-8">
                    {props.main}
                </main>
            </div>
        </div>
    </div>);
}