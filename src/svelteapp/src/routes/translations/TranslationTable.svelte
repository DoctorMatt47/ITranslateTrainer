<script lang="ts">
  import CreateTranslationRow from "./AddTranslationRow.svelte";
  import TranslationRow from "./TranslationRow.svelte";
  import { translationsStore } from "../../lib/stores";

  const heads = ["#", "From", "To", "Original", "Translation", "Action"];

</script>

<div class="table-container">
  <table class="table dark:bg-black/0">

    <thead>
    <tr class="bg-surface-500/50">
      {#each heads as head}
        <th class="text-center">{head}</th>
      {/each}
    </tr>
    </thead>

    <tbody class="table-body">
    <CreateTranslationRow />
    {#each $translationsStore as translation, rowIndex (translation.id)}
      <TranslationRow {translation} {rowIndex} />
    {/each}
    </tbody>

  </table>
</div>

<style>
    .dark .table thead {
        background: inherit;
    }
</style>
