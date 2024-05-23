<script lang="ts">
  import type { Snippet } from "svelte";

  const { children, class: _class = "", ...props }: {
    "class"?: string;
    children?: Snippet;
    [key: string]: unknown;
  } = $props();
</script>

<td {...props} class="text-center align-middle {_class}">
  {@render children()}
</td>
