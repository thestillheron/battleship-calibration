export const Errors: React.FC<{ errors: string[] }> = ({ errors }) => (
  <>
    {errors && errors.length > 0 && (
      <div className="errors">
        <h2>Errors</h2>
        {errors.map((error, i) => (
          <p key={i}>{error}</p>
        ))}
      </div>
    )}
  </>
);
