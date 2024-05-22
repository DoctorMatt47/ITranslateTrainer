<script lang="ts">
  import type { Snippet } from "svelte";

  const { padding = 4, children }: {
    padding?: number;
    children: Snippet;
  } = $props();
</script>

<div class="variant-ghost-surface p-{padding} rounded text-center">
  {@render children()}
</div>
