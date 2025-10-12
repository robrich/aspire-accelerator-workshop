import FrameworkTable from '@/components/FrameworkTable';

export default function App() {
  return (
    <>
      <div className="container">
        <header className="d-flex flex-wrap justify-content-center py-3 mb-4 border-bottom">
          <a href="/" className="d-flex align-items-center mb-3 mb-md-0 me-md-auto link-body-emphasis text-decoration-none">
            <svg className="bi me-2" width="40" height="32" aria-hidden="true">
              <use xlinkHref="#bootstrap"></use>
            </svg>
            <span className="fs-4">Framework Bakeoff</span>
          </a>
        </header>
      </div>
      <div className="container">
        <FrameworkTable />
      </div>
    </>
  );
}
