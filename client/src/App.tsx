import Layout from './layouts/two-column'
import Sidebar from './components/navigation/sidebar'
import GlobalNavigation from './components/navigation/global-navigation'
import { RssItemList as RssList } from './components/rssItems/rssItemList'
import { Article } from './components/rssItems/article'

import { useLoadPage } from './services/service'

export default function App() {
  const viewModel = useLoadPage(location.pathname);

  return (
    <>
      <GlobalNavigation />
      <Layout aside={<Sidebar data={viewModel.subscriptions} />}>
        {viewModel.content.kind === 'list' && <RssList data={viewModel.content.data} onChange={viewModel.refresh} />}
        {viewModel.content.kind === 'item' && <Article data={viewModel.content.data} />}
      </Layout>
    </>
  )
}
